CREATE DATABASE "MiniBankDB";
CREATE TABLE "account" (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    sum decimal NOT NULL,
    currency VARCHAR ( 50 ) NOT NULL,
    is_active bit NOT NULL,
    opening_date DateTime NOT NULL,
    closing_date DateTime NOT NULL
);
CREATE TABLE "user" (
    id UUID PRIMARY KEY,
    login VARCHAR ( 50 ) NOT NULL,
    email VARCHAR ( 50 ) NOT NULL
);
CREATE TABLE "remittanceHistory" (
    id UUID PRIMARY KEY,
    sum decimal NOT NULL,
    currency VARCHAR ( 50 ) NOT NULL,
    from_account_id UUID NOT NULL,
    to_account_id UUID NOT NULL
);