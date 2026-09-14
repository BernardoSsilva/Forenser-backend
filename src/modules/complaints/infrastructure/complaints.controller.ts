import { Body, Controller, HttpCode, HttpStatus, Post } from '@nestjs/common';
import { ApiTags } from '@nestjs/swagger';
import { CreateComplaintUseCase } from '../application/use-cases/create-complaint.use-case';
import { CreateComplaintDto } from '../application/dtos/create-complaint.dto';

@ApiTags('complaints')
@Controller('complaints')
export class ComplaintsController {
  constructor(private readonly createComplaint: CreateComplaintUseCase) {}

  @Post()
  @HttpCode(HttpStatus.CREATED)
  create(@Body() dto: CreateComplaintDto) {
    return this.createComplaint.execute(dto);
  }
}
