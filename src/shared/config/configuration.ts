export default () => ({
  port: parseInt(process.env.PORT ?? '3001', 10),
  database: {
    url: process.env.DATABASE_URL,
  },
  jwt: {
    secret: process.env.JWT_SECRET,
    expiresIn: process.env.JWT_EXPIRES_IN ?? '1h',
  },
  openai: {
    apiKey: process.env.OPENAI_API_KEY,
  },
  corsOrigin: process.env.CORS_ORIGIN,
});
