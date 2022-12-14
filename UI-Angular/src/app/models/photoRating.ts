export class PhotoRating{
    id!: number;
    photoId: number;
    userId: number;
    grade: number;
    ratingDate: Date;

    constructor(photoId: number, userId: number, grade: number, ratingDate: Date){
        this.grade = grade;
        this.userId = userId;
        this.photoId = photoId;
        this.ratingDate = ratingDate;
    }
}