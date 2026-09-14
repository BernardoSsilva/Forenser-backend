import { ApiProperty } from '@nestjs/swagger';
import { IsString, IsUrl } from 'class-validator';

export class SaveFaceDto {
  @ApiProperty()
  @IsUrl()
  imageUrl: string;

  @ApiProperty()
  @IsString()
  description: string;

  @ApiProperty()
  @IsString()
  incidentReportId: string;
}
