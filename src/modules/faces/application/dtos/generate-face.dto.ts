import { ApiProperty } from '@nestjs/swagger';
import { IsString } from 'class-validator';

export class GenerateFaceDto {
  @ApiProperty() @IsString() sex: string;
  @ApiProperty() @IsString() ageGroup: string;
  @ApiProperty() @IsString() skinColor: string;
  @ApiProperty() @IsString() bodyType: string;
  @ApiProperty() @IsString() faceShape: string;
  @ApiProperty() @IsString() headShape: string;
  @ApiProperty() @IsString() hairHeight: string;
  @ApiProperty() @IsString() hairType: string;
  @ApiProperty() @IsString() hairColor: string;
  @ApiProperty() @IsString() hairStyle: string;
  @ApiProperty() @IsString() beard: string;
  @ApiProperty() @IsString() beardStyle: string;
  @ApiProperty() @IsString() eyeShape: string;
  @ApiProperty() @IsString() eyeColor: string;
  @ApiProperty() @IsString() mouthShape: string;
  @ApiProperty() @IsString() noseShape: string;
  @ApiProperty() @IsString() chinShape: string;
  @ApiProperty() @IsString() earShape: string;
  @ApiProperty() @IsString() ethnicity: string;
  @ApiProperty() @IsString() accessories: string;
  @ApiProperty() @IsString() facialMarks: string;
}
