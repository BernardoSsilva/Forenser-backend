import { plainToInstance } from 'class-transformer';
import {
  IsIn,
  IsNotEmpty,
  IsNumberString,
  IsOptional,
  validateSync,
} from 'class-validator';

class EnvironmentVariables {
  @IsNotEmpty()
  DATABASE_URL: string;

  @IsNotEmpty()
  JWT_SECRET: string;

  @IsOptional()
  JWT_EXPIRES_IN?: string;

  @IsNotEmpty()
  OPENAI_API_KEY: string;

  @IsOptional()
  @IsNumberString()
  PORT?: string;

  @IsOptional()
  @IsIn(['production', 'development', 'test'])
  NODE_ENV?: string;
}

export function validate(config: Record<string, unknown>) {
  const validatedConfig = plainToInstance(EnvironmentVariables, config, {
    enableImplicitConversion: true,
  });
  const errors = validateSync(validatedConfig, {
    skipMissingProperties: false,
  });

  if (errors.length > 0) {
    throw new Error(
      `Configuração de ambiente inválida: ${errors.map((e) => e.toString()).join('; ')}`,
    );
  }
  return validatedConfig;
}
