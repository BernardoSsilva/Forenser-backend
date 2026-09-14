import { ApiProperty } from '@nestjs/swagger';
import { IsBoolean, IsString } from 'class-validator';
import { BaseIncidentReportDto } from './base-incident-report.dto';

export class CreateDomesticViolenceDto extends BaseIncidentReportDto {
  @ApiProperty({ description: 'Houve uso de violência durante a ocorrência?' })
  @IsBoolean()
  involvedViolence: boolean;

  @ApiProperty()
  @IsString()
  victimName: string;
}
