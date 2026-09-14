import { Injectable, UnauthorizedException } from '@nestjs/common';
import { UserRepositoryPort } from '../../../users/domain/ports/user.repository.port';
import { PasswordHasherPort } from '../../domain/ports/password-hasher.port';
import { TokenServicePort } from '../../domain/ports/token.port';
import { InvalidCredentialsError } from '../../domain/errors/invalid-credentials.error';
import { LoginDto } from '../dtos/login.dto';
import { PublicUserProfile } from '../../../users/domain/entities/user.entity';

export interface LoginResult {
  accessToken: string;
  user: PublicUserProfile;
}

@Injectable()
export class LoginUseCase {
  constructor(
    private readonly userRepository: UserRepositoryPort,
    private readonly passwordHasher: PasswordHasherPort,
    private readonly tokenService: TokenServicePort,
  ) {}

  async execute(dto: LoginDto): Promise<LoginResult> {
    const user = await this.userRepository.findByEmail(dto.email);
    if (!user) {
      throw new UnauthorizedException(new InvalidCredentialsError().message);
    }

    const passwordMatches = await this.passwordHasher.compare(
      dto.password,
      user.passwordHash,
    );
    if (!passwordMatches) {
      throw new UnauthorizedException(new InvalidCredentialsError().message);
    }

    const accessToken = this.tokenService.sign({ sub: user.id });

    return { accessToken, user: user.toPublicProfile() };
  }
}
