import { Body, Controller, Post, UseGuards } from '@nestjs/common';
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger';
import { JwtAuthGuard } from '../../../shared/guards/jwt-auth.guard';
import {
  CurrentUser,
  AuthenticatedUser,
} from '../../../shared/decorators/current-user.decorator';
import { GenerateFaceImageUseCase } from '../application/use-cases/generate-face-image.use-case';
import { SaveFaceUseCase } from '../application/use-cases/save-face.use-case';
import { GenerateFaceDto } from '../application/dtos/generate-face.dto';
import { SaveFaceDto } from '../application/dtos/save-face.dto';

@ApiTags('faces')
@ApiBearerAuth()
@UseGuards(JwtAuthGuard)
@Controller('faces')
export class FacesController {
  constructor(
    private readonly generateFaceImage: GenerateFaceImageUseCase,
    private readonly saveFace: SaveFaceUseCase,
  ) {}

  @Post('generate')
  generate(@Body() dto: GenerateFaceDto) {
    return this.generateFaceImage.execute(dto);
  }

  @Post()
  save(@CurrentUser() user: AuthenticatedUser, @Body() dto: SaveFaceDto) {
    return this.saveFace.execute(user.id, dto);
  }
}
