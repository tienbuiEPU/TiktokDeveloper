export function convertDate(date: string) {
    if (date) {
        let _date = new Date(date);
        let month = _date.getMonth() + 1;

        return `${_date.getFullYear()}-${month < 10 ? '0' + month : month}-${_date.getDate()}`;
    }

    return undefined;
}