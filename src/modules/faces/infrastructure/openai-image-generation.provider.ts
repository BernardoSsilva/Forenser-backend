import { Injectable, InternalServerErrorException } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import OpenAI from 'openai';
import {
  GeneratedImage,
  ImageGenerationPort,
} from '../domain/ports/image-generation.port';

@Injectable()
export class OpenAiImageGenerationProvider implements ImageGenerationPort {
  private readonly client: OpenAI;

  constructor(configService: ConfigService) {
    this.client = new OpenAI({
      apiKey: configService.get<string>('openai.apiKey'),
    });
  }

  async generate(prompt: string): Promise<GeneratedImage> {
    const response = await this.client.images.generate({
      model: 'dall-e-3',
      prompt,
      n: 1,
      size: '1024x1024',
    });

    const imageUrl = response.data?.[0]?.url;
    if (!imageUrl) {
      throw new InternalServerErrorException(
        'Não foi possível gerar a imagem com a IA. Tente novamente.',
      );
    }

    return { imageUrl };
  }
}
