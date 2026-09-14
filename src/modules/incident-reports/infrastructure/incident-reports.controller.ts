import { Body, Controller, Get, Param, Post, UseGuards } from '@nestjs/common';
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger';
import { JwtAuthGuard } from '../../../shared/guards/jwt-auth.guard';
import {
  CurrentUser,
  AuthenticatedUser,
} from '../../../shared/decorators/current-user.decorator';
import { CreateTrafficAccidentReportUseCase } from '../application/use-cases/create-traffic-accident-report.use-case';
import { CreateTheftReportUseCase } from '../application/use-cases/create-theft-report.use-case';
import { CreateDomesticViolenceReportUseCase } from '../application/use-cases/create-domestic-violence-report.use-case';
import { ListUserIncidentReportsUseCase } from '../application/use-cases/list-user-incident-reports.use-case';
import { GetUserIncidentReportUseCase } from '../application/use-cases/get-user-incident-report.use-case';
import { CreateTrafficAccidentDto } from '../application/dtos/create-traffic-accident.dto';
import { CreateTheftDto } from '../application/dtos/create-theft.dto';
import { CreateDomesticViolenceDto } from '../application/dtos/create-domestic-violence.dto';

@ApiTags('incident-reports')
@ApiBearerAuth()
@UseGuards(JwtAuthGuard)
@Controller('incident-reports')
export class IncidentReportsController {
  constructor(
    private readonly createTrafficAccidentReport: CreateTrafficAccidentReportUseCase,
    private readonly createTheftReport: CreateTheftReportUseCase,
    private readonly createDomesticViolenceReport: CreateDomesticViolenceReportUseCase,
    private readonly listUserIncidentReports: ListUserIncidentReportsUseCase,
    private readonly getUserIncidentReport: GetUserIncidentReportUseCase,
  ) {}

  @Post('traffic-accidents')
  createTrafficAccident(
    @CurrentUser() user: AuthenticatedUser,
    @Body() dto: CreateTrafficAccidentDto,
  ) {
    return this.createTrafficAccidentReport.execute(user.id, dto);
  }

  @Post('thefts')
  createTheft(
    @CurrentUser() user: AuthenticatedUser,
    @Body() dto: CreateTheftDto,
  ) {
    return this.createTheftReport.execute(user.id, dto);
  }

  @Post('domestic-violence')
  createDomesticViolence(
    @CurrentUser() user: AuthenticatedUser,
    @Body() dto: CreateDomesticViolenceDto,
  ) {
    return this.createDomesticViolenceReport.execute(user.id, dto);
  }

  @Get()
  findMine(@CurrentUser() user: AuthenticatedUser) {
    return this.listUserIncidentReports.execute(user.id);
  }

  @Get(':id')
  findOne(@CurrentUser() user: AuthenticatedUser, @Param('id') id: string) {
    return this.getUserIncidentReport.execute(user.id, id);
  }
}
