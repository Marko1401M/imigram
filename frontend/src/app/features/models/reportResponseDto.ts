import { PostAll } from "./post-all";
import { UserDto } from "./userDto";

export interface ReportResponse{
    id: string,
    reporter: UserDto,
    post: PostAll,
    reason: string,
    description: string,
    createdAt: Date,
    status: string,
    resolvedBy: UserDto,
    resolvedAt: Date
};