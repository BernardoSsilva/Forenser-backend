import { ApiProperty } from '@nestjs/swagger';
import { IsBoolean, IsString } from 'class-validator';
import { BaseIncidentReportDto } from './base-incident-report.dto';

export class CreateTheftDto extends BaseIncidentReportDto {
  @ApiProperty({ description: 'Houve uso de violência ou grave ameaça?' })
  @IsBoolean()
  involvedViolence: boolean;

  @ApiProperty({ description: 'O bem chegou a ser subtraído?' })
  @IsBoolean()
  propertyTaken: boolean;

  @ApiProperty()
  @IsString()
  victimName: string;

  @ApiProperty()
  @IsString()
  stolenItems: string;
}
