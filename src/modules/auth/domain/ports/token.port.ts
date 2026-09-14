export interface TokenPayload {
  sub: string;
}

export abstract class TokenServicePort {
  abstract sign(payload: TokenPayload): string;
}
