import { Injectable } from '@nestjs/common';
import { UserRepositoryPort } from '../../domain/ports/user.repository.port';
import { UserNotFoundError } from '../../domain/errors/user-domain.errors';
import { UpdateUserDto } from '../dtos/update-user.dto';
import { PublicUserProfile } from '../../domain/entities/user.entity';

@Injectable()
export class UpdateUserProfileUseCase {
  constructor(private readonly userRepository: UserRepositoryPort) {}

  async execute(
    userId: string,
    data: UpdateUserDto,
  ): Promise<PublicUserProfile> {
    const user = await this.userRepository.findById(userId);
    if (!user) {
      throw new UserNotFoundError();
    }

    const updated = await this.userRepository.update(userId, {
      email: data.email,
      phoneNumber: data.phoneNumber,
    });
    return updated.toPublicProfile();
  }
}
