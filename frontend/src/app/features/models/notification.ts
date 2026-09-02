export interface Notification{
    id: string,
    userId: string,
    senderId: string,
    type: string,
    message: string,
    isRead: boolean,
    postId?: string,
    commentId?: string,
    createdAt: Date
};