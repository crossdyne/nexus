export interface SearchUserResponse {
    inviteCode: string;
    userName: string;
    isISend: boolean;
    isIWasSend: boolean;
    isFriend: boolean;
    avatarUrl?: string;
}