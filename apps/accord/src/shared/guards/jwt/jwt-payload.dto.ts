export enum JwtType {
  INVITE = 'invitetoken',
  ACCESS = 'accesstoken',
  REFRESH = 'refreshtoken'
}

export interface IJwtPayloadDTO {
  id: string
  typ: JwtType
  sub?: string
  exp?: number
}
