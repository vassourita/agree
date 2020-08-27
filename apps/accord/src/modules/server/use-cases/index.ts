import { DecodeInviteTokenUseCase } from './decode-invite-token/decode-invite-token.use-case'
import { SignInviteTokenUseCase } from './sign-invite-token/sign-invite-token.use-case'

export const useCases = [SignInviteTokenUseCase, DecodeInviteTokenUseCase]
