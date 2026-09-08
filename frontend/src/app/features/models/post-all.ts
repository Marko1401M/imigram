import { PostLocation } from "./location";
import { PostMedia } from "./postMedia";

export interface PostAll {
  id: string;
  userId: string;

  fullName: string;
  username: string;
  profileImage: string;

  content?: string;

  media: PostMedia[];

  createdAt: Date;

  likesCount: number;
  isLiked: boolean;

  commentsCount: number;

  location?: PostLocation;

  isDeleted: boolean;
};

