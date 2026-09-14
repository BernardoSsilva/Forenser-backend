export type Sex = 'MALE' | 'FEMALE';

export class UserEntity {
  constructor(
    public readonly id: string,
    public name: string,
    public email: string,
    public passwordHash: string,
    public phoneNumber: string,
    public readonly cpf: string,
    public readonly sex: Sex,
    public readonly birthDate: Date,
    public readonly createdAt: Date,
  ) {}

  toPublicProfile() {
    return {
      id: this.id,
      name: this.name,
      email: this.email,
      phoneNumber: this.phoneNumber,
      cpf: this.cpf,
      sex: this.sex,
      birthDate: this.birthDate,
      createdAt: this.createdAt,
    };
  }
}

export type PublicUserProfile = ReturnType<UserEntity['toPublicProfile']>;
