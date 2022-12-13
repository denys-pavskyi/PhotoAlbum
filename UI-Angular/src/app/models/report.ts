export class Report{
    id!: number;
    comment!: string;
    photoId: number;
    userId: number;
    status: ReportStatus;

    constructor(comment="", photoId:number,userId:number, status: ReportStatus ){
        this.comment = comment;
        this.photoId = photoId;
        this.userId = userId;
        this.status = status;
    }
}

export enum ReportStatus{
        Approved = 0,
        Declined = 1,
        OnReview = 2
}