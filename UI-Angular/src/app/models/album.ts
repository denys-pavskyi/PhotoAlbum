export class Album{
    id!:number;
    title: string;
    description: string;
    numberOfPictures: number;
    userId: number;
    creationDate: Date;
    albumPhotoIds: Array<number>;

    constructor(title:string, description:string, numberOfPictures:number
        , userId: number, creationDate:Date, albumPhotoIds: Array<number>){

            this.title = title;
            this.description = description;
            this.numberOfPictures = numberOfPictures;
            this.userId = userId;
            this.creationDate = creationDate;
            this.albumPhotoIds = albumPhotoIds;
        }
}