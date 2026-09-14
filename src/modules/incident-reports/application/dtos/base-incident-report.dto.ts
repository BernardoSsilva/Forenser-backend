import { ApiProperty } from '@nestjs/swagger';
import { Type } from 'class-transformer';
import { IsDate, IsIn, IsString } from 'class-validator';
import { LocationType } from '../../domain/entities/incident-report.entity';

export abstract class BaseIncidentReportDto {
  @ApiProperty()
  @Type(() => Date)
  @IsDate()
  occurredAt: Date;

  @ApiProperty()
  @IsString()
  occurredTime: string;

  @ApiProperty({ enum: ['RAILWAY', 'PUBLIC_ROAD', 'OTHER'] })
  @IsIn(['RAILWAY', 'PUBLIC_ROAD', 'OTHER'])
  locationType: LocationType;

  @ApiProperty()
  @IsString()
  address: string;

  @ApiProperty()
  @IsString()
  informantName: string;

  @ApiProperty()
  @IsString()
  narrative: string;
}
