export interface AuthResponse {
  token: string;
  refreshToken: string;
  refreshTokenExpiryTime: string; // ISO date string
}