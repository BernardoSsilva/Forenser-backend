import { ApiProperty } from '@nestjs/swagger';
import { Type } from 'class-transformer';
import { IsDate, IsEmail, IsIn, IsString, MinLength } from 'class-validator';

export class RegisterDto {
  @ApiProperty()
  @IsString()
  name: string;

  @ApiProperty()
  @IsEmail({}, { message: 'E-mail inválido' })
  email: string;

  @ApiProperty()
  @IsString()
  @MinLength(8, { message: 'A senha deve ter ao menos 8 caracteres' })
  password: string;

  @ApiProperty()
  @IsString()
  phoneNumber: string;

  @ApiProperty()
  @IsString()
  cpf: string;

  @ApiProperty({ enum: ['MALE', 'FEMALE'] })
  @IsIn(['MALE', 'FEMALE'])
  sex: 'MALE' | 'FEMALE';

  @ApiProperty()
  @Type(() => Date)
  @IsDate()
  birthDate: Date;
}
