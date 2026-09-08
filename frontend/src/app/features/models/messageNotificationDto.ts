export interface MessageNotificationDto{
    id: string,
    chatId: string,
    senderId: string,
    senderUsername: string,
    senderFullName: string,
    senderProfileImage: string,
    content: string,
    sentAt: Date,
    isRead: boolean
};