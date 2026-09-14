import { UserEntity } from '../entities/user.entity';

export interface CreateUserData {
  name: string;
  email: string;
  passwordHash: string;
  phoneNumber: string;
  cpf: string;
  sex: 'MALE' | 'FEMALE';
  birthDate: Date;
}

export interface UpdateUserData {
  email?: string;
  phoneNumber?: string;
}

export abstract class UserRepositoryPort {
  abstract create(data: CreateUserData): Promise<UserEntity>;
  abstract findById(id: string): Promise<UserEntity | null>;
  abstract findByEmail(email: string): Promise<UserEntity | null>;
  abstract findByEmailPhoneOrCpf(
    email: string,
    phoneNumber: string,
    cpf: string,
  ): Promise<UserEntity | null>;
  abstract update(id: string, data: UpdateUserData): Promise<UserEntity>;
  abstract delete(id: string): Promise<void>;
}
