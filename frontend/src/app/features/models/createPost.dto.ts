import { PostLocation } from "./location";

export interface CreatePostDto{
    content?: string;
    location?: PostLocation;
    media: File[];
}

