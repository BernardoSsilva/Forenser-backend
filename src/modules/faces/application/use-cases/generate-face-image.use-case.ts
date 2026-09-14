import { Injectable } from '@nestjs/common';
import { ImageGenerationPort } from '../../domain/ports/image-generation.port';
import { FacePromptBuilder } from '../../domain/services/face-prompt-builder.service';
import { GenerateFaceDto } from '../dtos/generate-face.dto';

export interface GenerateFaceResult {
  imageUrl: string;
  description: string;
}

@Injectable()
export class GenerateFaceImageUseCase {
  constructor(
    private readonly imageGeneration: ImageGenerationPort,
    private readonly promptBuilder: FacePromptBuilder,
  ) {}

  async execute(dto: GenerateFaceDto): Promise<GenerateFaceResult> {
    const prompt = this.promptBuilder.buildImagePrompt(dto);
    const description = this.promptBuilder.buildDescription(dto);

    const { imageUrl } = await this.imageGeneration.generate(prompt);

    return { imageUrl, description };
  }
}
