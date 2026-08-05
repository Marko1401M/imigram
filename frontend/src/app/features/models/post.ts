import { PostMedia } from "./postMedia";
import { PostLocation } from "./location";
export interface Post{
    id: string;
    userId: string;
    content?: string;
    media: PostMedia[];
    createdAt: Date;
    likedBy: string[];
    commentsCount:number;
    location?: PostLocation;
}