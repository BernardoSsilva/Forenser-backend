import { Module } from '@nestjs/common';
import { UsersController } from './infrastructure/users.controller';
import { PrismaUserRepository } from './infrastructure/prisma-user.repository';
import { UserRepositoryPort } from './domain/ports/user.repository.port';
import { GetUserProfileUseCase } from './application/use-cases/get-user-profile.use-case';
import { UpdateUserProfileUseCase } from './application/use-cases/update-user-profile.use-case';
import { DeleteUserUseCase } from './application/use-cases/delete-user.use-case';

@Module({
  controllers: [UsersController],
  providers: [
    { provide: UserRepositoryPort, useClass: PrismaUserRepository },
    GetUserProfileUseCase,
    UpdateUserProfileUseCase,
    DeleteUserUseCase,
  ],
  exports: [UserRepositoryPort],
})
export class UsersModule {}
