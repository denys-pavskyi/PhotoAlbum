import { raceWith } from "rxjs";

export class Photo{
    id!: number;
    photoPath: string;
    title: string;
    description?: string;
    uploadDate: Date;
    userId: number;
    totalRating: number;
    userName: string;
    photoRatingIds: Array<number>;
    photoIds: Array<number>;
    albumIds: Array<number>;


    constructor(photoPath: string, title: string, description="", uploadDate: Date, userId: number
    , totalRating: number, photoRatingIds: Array<number>, photoIds: Array<number>, albumIds: Array<number>,
    userName: string ){
        this.photoPath = photoPath;
        this.title = title;
        this.description = description;
        this.userId = userId;
        this.uploadDate = uploadDate;
        this.totalRating = totalRating;
        this.photoRatingIds = photoRatingIds;
        this.photoIds = photoIds;
        this.albumIds = albumIds;
        this.userName = userName;
    }
}