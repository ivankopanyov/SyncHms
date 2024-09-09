export const SERVER_HOST = import.meta.env.MODE === "development"
    ? "http://localhost:8080" : window.location.origin;

export const NAME_MIN = 4;

export const NAME_MAX = 50;

export const PASS_MIN = 6;

export const PASS_MAX = 50;
