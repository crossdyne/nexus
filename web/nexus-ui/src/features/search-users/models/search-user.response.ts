export interface SearchUserResponse {
    userId: string;
    userName: string;
    isISend: boolean;
    isIWasSend: boolean;
    isFriend: boolean;
    avatarUrl?: string;
}