import { FacePromptBuilder } from './face-prompt-builder.service';
import { FacialAttributes } from '../value-objects/facial-attributes';

describe('FacePromptBuilder', () => {
  const attributes: FacialAttributes = {
    sex: 'woman',
    ageGroup: 'young adult',
    skinColor: 'olive',
    bodyType: 'lean',
    faceShape: 'oval',
    headShape: 'oval',
    hairHeight: 'long',
    hairType: 'wavy',
    hairColor: 'black',
    hairStyle: 'ponytail',
    beard: 'none',
    beardStyle: 'degrade',
    eyeShape: 'almond',
    eyeColor: 'brown',
    mouthShape: 'medium',
    noseShape: 'pointed',
    chinShape: 'rounded',
    earShape: 'small',
    ethnicity: 'hispanic',
    accessories: 'none',
    facialMarks: 'none',
  };

  const builder = new FacePromptBuilder();

  it('builds a plain-language description from the attributes', () => {
    const description = builder.buildDescription(attributes);

    expect(description).toContain('young adult olive woman');
    expect(description).toContain('hispanic ethnicity');
    expect(description).not.toContain('beard style');
  });

  it('omits accessories and facial marks when set to none', () => {
    const description = builder.buildDescription(attributes);

    expect(description).not.toContain('wearing');
    expect(description).not.toContain('with none');
  });

  it('includes accessories and facial marks when present', () => {
    const description = builder.buildDescription({
      ...attributes,
      accessories: 'glasses',
      facialMarks: 'scars',
    });

    expect(description).toContain('wearing glasses');
    expect(description).toContain('with scars');
  });

  it('wraps the description into a forensic composite image prompt', () => {
    const prompt = builder.buildImagePrompt(attributes);

    expect(prompt).toContain('forensic composite portrait');
    expect(prompt).toContain(builder.buildDescription(attributes));
  });
});
