import {
  Body,
  Controller,
  Delete,
  Get,
  HttpCode,
  HttpStatus,
  Patch,
  UseGuards,
} from '@nestjs/common';
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger';
import { JwtAuthGuard } from '../../../shared/guards/jwt-auth.guard';
import {
  CurrentUser,
  AuthenticatedUser,
} from '../../../shared/decorators/current-user.decorator';
import { GetUserProfileUseCase } from '../application/use-cases/get-user-profile.use-case';
import { UpdateUserProfileUseCase } from '../application/use-cases/update-user-profile.use-case';
import { DeleteUserUseCase } from '../application/use-cases/delete-user.use-case';
import { UpdateUserDto } from '../application/dtos/update-user.dto';

@ApiTags('users')
@ApiBearerAuth()
@UseGuards(JwtAuthGuard)
@Controller('users')
export class UsersController {
  constructor(
    private readonly getUserProfile: GetUserProfileUseCase,
    private readonly updateUserProfile: UpdateUserProfileUseCase,
    private readonly deleteUser: DeleteUserUseCase,
  ) {}

  @Get('me')
  me(@CurrentUser() user: AuthenticatedUser) {
    return this.getUserProfile.execute(user.id);
  }

  @Patch('me')
  updateMe(@CurrentUser() user: AuthenticatedUser, @Body() dto: UpdateUserDto) {
    return this.updateUserProfile.execute(user.id, dto);
  }

  @Delete('me')
  @HttpCode(HttpStatus.NO_CONTENT)
  deleteMe(@CurrentUser() user: AuthenticatedUser) {
    return this.deleteUser.execute(user.id);
  }
}
