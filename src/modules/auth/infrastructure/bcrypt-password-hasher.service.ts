import { Injectable } from '@nestjs/common';
import * as bcrypt from 'bcrypt';
import { PasswordHasherPort } from '../domain/ports/password-hasher.port';

const SALT_ROUNDS = 10;

@Injectable()
export class BcryptPasswordHasher implements PasswordHasherPort {
  hash(plainText: string): Promise<string> {
    return bcrypt.hash(plainText, SALT_ROUNDS);
  }

  compare(plainText: string, hash: string): Promise<boolean> {
    return bcrypt.compare(plainText, hash);
  }
}
