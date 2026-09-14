import { UnauthorizedException } from '@nestjs/common';
import { LoginUseCase } from './login.use-case';
import { UserRepositoryPort } from '../../../users/domain/ports/user.repository.port';
import { PasswordHasherPort } from '../../domain/ports/password-hasher.port';
import { TokenServicePort } from '../../domain/ports/token.port';
import { UserEntity } from '../../../users/domain/entities/user.entity';

describe('LoginUseCase', () => {
  const user = new UserEntity(
    'user-1',
    'Maria Silva',
    'maria@example.com',
    'hashed-password',
    '11999999999',
    '12345678900',
    'FEMALE',
    new Date('1990-01-01'),
    new Date(),
  );

  let userRepository: jest.Mocked<UserRepositoryPort>;
  let passwordHasher: jest.Mocked<PasswordHasherPort>;
  let tokenService: jest.Mocked<TokenServicePort>;
  let useCase: LoginUseCase;

  beforeEach(() => {
    userRepository = {
      findByEmail: jest.fn(),
    } as unknown as jest.Mocked<UserRepositoryPort>;
    passwordHasher = {
      compare: jest.fn(),
    } as unknown as jest.Mocked<PasswordHasherPort>;
    tokenService = {
      sign: jest.fn(),
    } as unknown as jest.Mocked<TokenServicePort>;
    useCase = new LoginUseCase(userRepository, passwordHasher, tokenService);
  });

  it('returns an access token when credentials are valid', async () => {
    userRepository.findByEmail.mockResolvedValue(user);
    passwordHasher.compare.mockResolvedValue(true);
    tokenService.sign.mockReturnValue('signed-token');

    const result = await useCase.execute({
      email: user.email,
      password: 'senha1234',
    });

    expect(tokenService.sign).toHaveBeenCalledWith({ sub: user.id });
    expect(result.accessToken).toBe('signed-token');
    expect(result.user.email).toBe(user.email);
  });

  it('rejects when the user does not exist', async () => {
    userRepository.findByEmail.mockResolvedValue(null);

    await expect(
      useCase.execute({ email: 'unknown@example.com', password: 'senha1234' }),
    ).rejects.toBeInstanceOf(UnauthorizedException);
  });

  it('rejects when the password does not match', async () => {
    userRepository.findByEmail.mockResolvedValue(user);
    passwordHasher.compare.mockResolvedValue(false);

    await expect(
      useCase.execute({ email: user.email, password: 'wrong-password' }),
    ).rejects.toBeInstanceOf(UnauthorizedException);
  });
});
