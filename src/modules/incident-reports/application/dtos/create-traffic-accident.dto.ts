import { ApiProperty } from '@nestjs/swagger';
import { IsString } from 'class-validator';
import { BaseIncidentReportDto } from './base-incident-report.dto';

export class CreateTrafficAccidentDto extends BaseIncidentReportDto {
  @ApiProperty()
  @IsString()
  driverName: string;

  @ApiProperty({ description: 'Placa, modelo e ano dos veículos envolvidos' })
  @IsString()
  vehiclesInvolved: string;
}
