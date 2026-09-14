export interface GeneratedImage {
  imageUrl: string;
}

export abstract class ImageGenerationPort {
  abstract generate(prompt: string): Promise<GeneratedImage>;
}
