export interface HamsterDto {
    id: number;
    name: string;
    description: string;
    weightInGrams: number;
    ageInMonths: number;
    personality: string;
    pricePerDay: number;
    isAvailable: boolean;
}

export interface BookingDto {
    id: number;
    customerName: string;
    customerEmail: string;
    customerAddress: string;
    hamsterId: number;
    hamster?: HamsterDto;
    startDate: string;
    endDate: string;
    purpose: string;
    daysBooked: number;
    totalPrice: number;
}

export interface CreateBookingRequest {
    customerName: string;
    customerEmail: string;
    customerAddress: string;
    hamsterId: number;
    startDate: string;
    endDate: string;
    purpose: string;
}

export interface AddHamsterRequest {
    name: string;
    description: string;
    weightInGrams: number;
    ageInMonths: number;
    personality: string;
    pricePerDay: number;
}