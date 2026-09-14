import { Module } from '@nestjs/common';
import { JwtModule } from '@nestjs/jwt';
import { PassportModule } from '@nestjs/passport';
import { ConfigModule, ConfigService } from '@nestjs/config';
import { UsersModule } from '../users/users.module';
import { AuthController } from './infrastructure/auth.controller';
import { BcryptPasswordHasher } from './infrastructure/bcrypt-password-hasher.service';
import { JwtTokenService } from './infrastructure/jwt-token.service';
import { JwtStrategy } from './infrastructure/jwt.strategy';
import { PasswordHasherPort } from './domain/ports/password-hasher.port';
import { TokenServicePort } from './domain/ports/token.port';
import { RegisterUseCase } from './application/use-cases/register.use-case';
import { LoginUseCase } from './application/use-cases/login.use-case';

@Module({
  imports: [
    UsersModule,
    PassportModule,
    JwtModule.registerAsync({
      imports: [ConfigModule],
      inject: [ConfigService],
      useFactory: (configService: ConfigService) => ({
        secret: configService.get<string>('jwt.secret'),
        signOptions: { expiresIn: configService.get<string>('jwt.expiresIn') },
      }),
    }),
  ],
  controllers: [AuthController],
  providers: [
    { provide: PasswordHasherPort, useClass: BcryptPasswordHasher },
    { provide: TokenServicePort, useClass: JwtTokenService },
    JwtStrategy,
    RegisterUseCase,
    LoginUseCase,
  ],
  exports: [PasswordHasherPort],
})
export class AuthModule {}
