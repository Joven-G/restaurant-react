package oidc

import (
	"net/http"

	"github.com/stretchr/testify/mock"
)

// JWKMockHTTPClient mocked client for testing
type JWKMockHTTPClient struct {
	mock.Mock
}

// Get returns mocked http for JWK
func (c *JWKMockHTTPClient) Get(url string) (*http.Response, error) {
	args := c.Called(url)
	return args.Get(0).(*http.Response), args.Error(1)
}
