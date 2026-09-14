import {
  Body,
  Controller,
  Delete,
  Get,
  HttpCode,
  HttpStatus,
  Param,
  Patch,
  Post,
  UseGuards,
} from '@nestjs/common';
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger';
import { JwtAuthGuard } from '../../../shared/guards/jwt-auth.guard';
import {
  CurrentUser,
  AuthenticatedUser,
} from '../../../shared/decorators/current-user.decorator';
import { CreateAppointmentUseCase } from '../application/use-cases/create-appointment.use-case';
import { ListUserAppointmentsUseCase } from '../application/use-cases/list-user-appointments.use-case';
import { UpdateAppointmentUseCase } from '../application/use-cases/update-appointment.use-case';
import { DeleteAppointmentUseCase } from '../application/use-cases/delete-appointment.use-case';
import { CreateAppointmentDto } from '../application/dtos/create-appointment.dto';
import { UpdateAppointmentDto } from '../application/dtos/update-appointment.dto';

@ApiTags('appointments')
@ApiBearerAuth()
@UseGuards(JwtAuthGuard)
@Controller('appointments')
export class AppointmentsController {
  constructor(
    private readonly createAppointment: CreateAppointmentUseCase,
    private readonly listUserAppointments: ListUserAppointmentsUseCase,
    private readonly updateAppointment: UpdateAppointmentUseCase,
    private readonly deleteAppointment: DeleteAppointmentUseCase,
  ) {}

  @Post()
  create(
    @CurrentUser() user: AuthenticatedUser,
    @Body() dto: CreateAppointmentDto,
  ) {
    return this.createAppointment.execute(user.id, dto);
  }

  @Get()
  findMine(@CurrentUser() user: AuthenticatedUser) {
    return this.listUserAppointments.execute(user.id);
  }

  @Patch(':id')
  update(
    @CurrentUser() user: AuthenticatedUser,
    @Param('id') id: string,
    @Body() dto: UpdateAppointmentDto,
  ) {
    return this.updateAppointment.execute(user.id, id, dto);
  }

  @Delete(':id')
  @HttpCode(HttpStatus.NO_CONTENT)
  remove(@CurrentUser() user: AuthenticatedUser, @Param('id') id: string) {
    return this.deleteAppointment.execute(user.id, id);
  }
}
