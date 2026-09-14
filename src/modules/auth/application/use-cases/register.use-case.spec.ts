import { ConflictException } from '@nestjs/common';
import { RegisterUseCase } from './register.use-case';
import { UserRepositoryPort } from '../../../users/domain/ports/user.repository.port';
import { PasswordHasherPort } from '../../domain/ports/password-hasher.port';
import { UserEntity } from '../../../users/domain/entities/user.entity';

describe('RegisterUseCase', () => {
  const dto = {
    name: 'Maria Silva',
    email: 'maria@example.com',
    password: 'senha1234',
    phoneNumber: '11999999999',
    cpf: '12345678900',
    sex: 'FEMALE' as const,
    birthDate: new Date('1990-01-01'),
  };

  let userRepository: jest.Mocked<UserRepositoryPort>;
  let passwordHasher: jest.Mocked<PasswordHasherPort>;
  let useCase: RegisterUseCase;

  beforeEach(() => {
    userRepository = {
      findByEmailPhoneOrCpf: jest.fn(),
      create: jest.fn(),
    } as unknown as jest.Mocked<UserRepositoryPort>;
    passwordHasher = {
      hash: jest.fn(),
    } as unknown as jest.Mocked<PasswordHasherPort>;
    useCase = new RegisterUseCase(userRepository, passwordHasher);
  });

  it('creates a user with a hashed password', async () => {
    userRepository.findByEmailPhoneOrCpf.mockResolvedValue(null);
    passwordHasher.hash.mockResolvedValue('hashed-password');
    userRepository.create.mockResolvedValue(
      new UserEntity(
        'user-1',
        dto.name,
        dto.email,
        'hashed-password',
        dto.phoneNumber,
        dto.cpf,
        dto.sex,
        dto.birthDate,
        new Date(),
      ),
    );

    const result = await useCase.execute(dto);

    expect(passwordHasher.hash).toHaveBeenCalledWith(dto.password);
    expect(userRepository.create).toHaveBeenCalledWith(
      expect.objectContaining({ passwordHash: 'hashed-password' }),
    );
    expect(result.email).toBe(dto.email);
    expect((result as any).passwordHash).toBeUndefined();
  });

  it('rejects when a user with the same email, phone or cpf already exists', async () => {
    userRepository.findByEmailPhoneOrCpf.mockResolvedValue(
      new UserEntity(
        'user-1',
        dto.name,
        dto.email,
        'hashed-password',
        dto.phoneNumber,
        dto.cpf,
        dto.sex,
        dto.birthDate,
        new Date(),
      ),
    );

    await expect(useCase.execute(dto)).rejects.toBeInstanceOf(
      ConflictException,
    );
    expect(userRepository.create).not.toHaveBeenCalled();
  });
});
