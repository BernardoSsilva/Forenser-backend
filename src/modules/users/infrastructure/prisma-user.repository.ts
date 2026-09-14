import { Injectable } from '@nestjs/common';
import { PrismaService } from '../../../shared/database/prisma.service';
import {
  CreateUserData,
  UpdateUserData,
  UserRepositoryPort,
} from '../domain/ports/user.repository.port';
import { UserEntity } from '../domain/entities/user.entity';
import { User as PrismaUser } from '@prisma/client';

@Injectable()
export class PrismaUserRepository implements UserRepositoryPort {
  constructor(private readonly prisma: PrismaService) {}

  async create(data: CreateUserData): Promise<UserEntity> {
    const created = await this.prisma.user.create({
      data: {
        name: data.name,
        email: data.email,
        passwordHash: data.passwordHash,
        phoneNumber: data.phoneNumber,
        cpf: data.cpf,
        sex: data.sex,
        birthDate: data.birthDate,
      },
    });
    return this.toEntity(created);
  }

  async findById(id: string): Promise<UserEntity | null> {
    const user = await this.prisma.user.findUnique({ where: { id } });
    return user ? this.toEntity(user) : null;
  }

  async findByEmail(email: string): Promise<UserEntity | null> {
    const user = await this.prisma.user.findUnique({ where: { email } });
    return user ? this.toEntity(user) : null;
  }

  async findByEmailPhoneOrCpf(
    email: string,
    phoneNumber: string,
    cpf: string,
  ): Promise<UserEntity | null> {
    const user = await this.prisma.user.findFirst({
      where: { OR: [{ email }, { phoneNumber }, { cpf }] },
    });
    return user ? this.toEntity(user) : null;
  }

  async update(id: string, data: UpdateUserData): Promise<UserEntity> {
    const updated = await this.prisma.user.update({
      where: { id },
      data: {
        email: data.email,
        phoneNumber: data.phoneNumber,
      },
    });
    return this.toEntity(updated);
  }

  async delete(id: string): Promise<void> {
    await this.prisma.user.delete({ where: { id } });
  }

  private toEntity(user: PrismaUser): UserEntity {
    return new UserEntity(
      user.id,
      user.name,
      user.email,
      user.passwordHash,
      user.phoneNumber,
      user.cpf,
      user.sex,
      user.birthDate,
      user.createdAt,
    );
  }
}
