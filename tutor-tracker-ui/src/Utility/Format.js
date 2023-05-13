function halfHourText(value) {
    const hours = String(Math.floor(value / 2)).padStart(2, '0');
    const mins = String((value % 2) * 30).padStart(2, '0');
    
    const text = `${hours}:${mins}`;
    return text;
}

function rateText(value) {
    return `£${value}`;
}

export { halfHourText, rateText }