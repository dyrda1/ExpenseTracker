import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    stages: [
        { duration: '30s', target: 100 },  // Ramp-up: 50 users in 30s
        { duration: '1m', target: 100 },   // Stay at 50 users for 1m
        { duration: '30s', target: 0 },   // Ramp-down: back to 0 users
    ],
};

export default function () {
    let res = http.get('https://idyrda.site');

    check(res, {
        'is status 200': (r) => r.status === 200,
        'response time < 200ms': (r) => r.timings.duration < 200,
    });

    sleep(1); // Simulate real user behavior with 1s wait time
}
