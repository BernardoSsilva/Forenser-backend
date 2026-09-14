import { ApiProperty } from '@nestjs/swagger';
import { Type } from 'class-transformer';
import { IsDate, IsString } from 'class-validator';

export class CreateAppointmentDto {
  @ApiProperty()
  @IsString()
  requesterName: string;

  @ApiProperty()
  @Type(() => Date)
  @IsDate()
  scheduledDate: Date;

  @ApiProperty()
  @IsString()
  scheduledTime: string;
}
