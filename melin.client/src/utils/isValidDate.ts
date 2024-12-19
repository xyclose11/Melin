export function isValidDate(dateString: Date | undefined): boolean {
    if (dateString === undefined) {
        return false;
    }
    const date = new Date(dateString);
    return !isNaN(date.getTime());
}
