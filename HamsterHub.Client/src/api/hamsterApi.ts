import axios from 'axios';
import type { HamsterDto, BookingDto, CreateBookingRequest, AddHamsterRequest } from '../types/types';

const api = axios.create({
    baseURL: 'http://localhost:5109/api'
});

// HAMSTER ENDPOINTS ----------------------------------------

export const getAllHamsters = async ():Promise<HamsterDto[]> => {
    const response = await api.get<HamsterDto[]>('/hamsters');
    return response.data;
}

export const getHamsterById = async (id: number):Promise<HamsterDto> => {
    const response = await api.get<HamsterDto>(`/hamsters/${id}`)
    return response.data;
}

export const getHamsterByPersonality = async (personality: string):Promise<HamsterDto[]> => {
    const response = await api.get<HamsterDto[]>(`/hamsters/personality/${personality}`)
    return response.data;
}
    
export const getCheapestHamster = async ():Promise<HamsterDto> => {
    const response = await api.get<HamsterDto>('/hamsters/cheapest')
    return response.data;
}

// POST

export const addHamster = async(request: AddHamsterRequest): Promise<void> => {
    await api.post('/hamsters', request);
}

// BOOKING ENDPOINTS ----------------------------------------

export const getAllBookings = async ():Promise<BookingDto[]> => {
    const response = await api.get<BookingDto[]>('/bookings')
    return response.data;
}

export const getBookingById = async (id: number):Promise<BookingDto> => {
    const response = await api.get<BookingDto>(`/bookings/${id}`)
    return response.data;
}

export const getBookingByHamsterId = async (hamsterId: number):Promise<BookingDto[]> => {
    const response = await api.get<BookingDto[]>(`/bookings/hamster/${hamsterId}`)
    return response.data;
}

export const getBookingByCustomerName = async (customerName: string):Promise<BookingDto[]> => {
    const response = await api.get<BookingDto[]>(`/bookings/customer/${customerName}`)
    return response.data;
}

export const getBookingByDate = async (date: string):Promise<BookingDto[]> => {
    const response = await api.get<BookingDto[]>(`/bookings/date/${date}`)
    return response.data;
}

// POST

export const createBooking = async (request: CreateBookingRequest): Promise<void> => {
    await api.post('/bookings', request);
}

// DELETE

export const deleteBooking = async (id: number): Promise<void> => {
    await api.delete(`/bookings/${id}`);
}