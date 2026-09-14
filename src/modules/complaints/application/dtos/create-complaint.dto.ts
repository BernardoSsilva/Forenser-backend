import { ApiProperty } from '@nestjs/swagger';
import { IsString } from 'class-validator';

export class CreateComplaintDto {
  @ApiProperty()
  @IsString()
  reporterName: string;

  @ApiProperty()
  @IsString()
  location: string;

  @ApiProperty()
  @IsString()
  description: string;
}
