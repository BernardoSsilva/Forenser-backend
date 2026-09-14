import { ConflictException, Injectable } from '@nestjs/common';
import { UserRepositoryPort } from '../../../users/domain/ports/user.repository.port';
import { UserAlreadyExistsError } from '../../../users/domain/errors/user-domain.errors';
import { PasswordHasherPort } from '../../domain/ports/password-hasher.port';
import { RegisterDto } from '../dtos/register.dto';
import { PublicUserProfile } from '../../../users/domain/entities/user.entity';

@Injectable()
export class RegisterUseCase {
  constructor(
    private readonly userRepository: UserRepositoryPort,
    private readonly passwordHasher: PasswordHasherPort,
  ) {}

  async execute(dto: RegisterDto): Promise<PublicUserProfile> {
    const existingUser = await this.userRepository.findByEmailPhoneOrCpf(
      dto.email,
      dto.phoneNumber,
      dto.cpf,
    );
    if (existingUser) {
      throw new ConflictException(new UserAlreadyExistsError().message);
    }

    const passwordHash = await this.passwordHasher.hash(dto.password);

    const user = await this.userRepository.create({
      name: dto.name,
      email: dto.email,
      passwordHash,
      phoneNumber: dto.phoneNumber,
      cpf: dto.cpf,
      sex: dto.sex,
      birthDate: dto.birthDate,
    });

    return user.toPublicProfile();
  }
}
