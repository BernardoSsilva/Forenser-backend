import { Injectable } from '@nestjs/common';
import { FacialAttributes } from '../value-objects/facial-attributes';

@Injectable()
export class FacePromptBuilder {
  buildDescription(attributes: FacialAttributes): string {
    return (
      `a ${attributes.ageGroup} ${attributes.skinColor} ${attributes.sex} with a ${attributes.bodyType} build, ` +
      `${attributes.faceShape} face, ${attributes.headShape} head, ${attributes.hairColor} ${attributes.hairHeight} ${attributes.hairType} hair ` +
      `styled as ${attributes.hairStyle}, ${attributes.beard} beard${
        attributes.beard !== 'none'
          ? ` in a ${attributes.beardStyle} style`
          : ''
      }, ` +
      `${attributes.eyeColor} ${attributes.eyeShape} eyes, ${attributes.mouthShape} mouth, ${attributes.noseShape} nose, ` +
      `${attributes.chinShape} chin, ${attributes.earShape} ears, of ${attributes.ethnicity} ethnicity` +
      `${attributes.accessories !== 'none' ? `, wearing ${attributes.accessories}` : ''}` +
      `${attributes.facialMarks !== 'none' ? `, with ${attributes.facialMarks}` : ''}.`
    );
  }

  buildImagePrompt(attributes: FacialAttributes): string {
    return (
      `A highly realistic forensic composite portrait, front-facing, neutral studio background, ` +
      `photographic style, of ${this.buildDescription(attributes)}`
    );
  }
}
