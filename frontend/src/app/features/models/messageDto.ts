export interface MessageDto{
    id: string,
    chatId: string,
    senderId: string,
    content: string,
    sentAt: Date,
    isRead: boolean
};