import { raceWith } from "rxjs";

export class Photo{
    id: number;
    photoUrl: string;
    title: string;
    description?: string;
    uploadDate: Date;
    userId: number;
    totalRating: number;
    userName: string;
    photoRatingIds: Array<number>;
    photoIds: Array<number>;
    albumIds: Array<number>;


    constructor(id: number, photoUrl: string, title: string, description="", uploadDate: Date, userId: number
    , totalRating: number, photoRatingIds: Array<number>, photoIds: Array<number>, albumIds: Array<number>,
    userName: string ){
        this.id = id;
        this.photoUrl = photoUrl;
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